# Copyright 2018 Google LLC
#
# Licensed under the Apache License, Version 2.0 (the "License");
# you may not use this file except in compliance with the License.
# You may obtain a copy of the License at
#
#     http://www.apache.org/licenses/LICENSE-2.0
#
# Unless required by applicable law or agreed to in writing, software
# distributed under the License is distributed on an "AS IS" BASIS,
# WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
# See the License for the specific language governing permissions and
# limitations under the License.

# setup:    https://developers.google.com/sheets/api/quickstart/python
# source:   https://github.com/gsuitedevs/python-samples/tree/master/sheets

from __future__ import print_function
import os
from oauth2client import file, client, tools
from googleapiclient.discovery import build
from httplib2 import Http

PATH = os.path.dirname(os.path.realpath(__file__))
SCOPES = 'https://www.googleapis.com/auth/spreadsheets'

class Sheet(object):
    def __init__(self, Spreadsheet_ID, name, service=False):
        self.Spreadsheet_ID=Spreadsheet_ID
        self.name=name
        if not service:
            store = file.Storage(os.path.join(PATH,*['token.json']))
            creds = store.get()
            if not creds or creds.invalid:
                flow = client.flow_from_clientsecrets(os.path.join(PATH,*['credentials.json']), SCOPES)
                creds = tools.run_flow(flow, store)
            service = build('sheets', 'v4', http=creds.authorize(Http()))
        self.SpreadsheetSnippets=SpreadsheetSnippets(service)

        self.values=self.get_values()

    def get_values(self, range_name='A:ZZZ', majorDimension='ROWS'):
        return self.SpreadsheetSnippets.get_values(self.Spreadsheet_ID,'%s!%s'%(self.name,range_name), majorDimension).get('values',[])

    def update(self, values, range_name='A:ZZZ', majorDimension='ROWS',value_input_option='RAW'):
        return self.SpreadsheetSnippets.update_values(self.Spreadsheet_ID, range_name, values, majorDimension,value_input_option)

    def append(self, values, range_name='A:ZZZ', majorDimension='ROWS',value_input_option='RAW'):
        return self.SpreadsheetSnippets.append_values(self.Spreadsheet_ID, range_name, values, majorDimension,value_input_option)

    def clear(self):
        self.update([['' for i in range(len(self.values[0]))]for i in range(len(self.values))])

    def convert(self,values=False):
        if not values:
            values=self.values
        header=values[0]
        data=[
            {
                key:row[i]
                for i,key in enumerate(header)
                if row[i]
            }
            for row in values[1:]
        ]
        return(data)

    def ObjListToSheet(self,objlist):
        self.clear()
        #//convert object into an 3D-array data=[row][column]
        header=[]
        for obj in objlist:
            for key in obj:
                if key not in header:
                    header.append(key)
        array=[
            [
                obj[key] if key in obj else None
                for key in header
            ]
            for obj in objlist
        ]
        array.unshift(header)     
        self.update(array)

    
class SpreadsheetSnippets(object):
    def __init__(self, service=False):
        if not service:
            store = file.Storage(os.path.join(PATH,*['token.json']))
            creds = store.get()
            if not creds or creds.invalid:
                flow = client.flow_from_clientsecrets(os.path.join(PATH,*['credentials.json']), SCOPES)
                creds = tools.run_flow(flow, store)
            service = build('sheets', 'v4', http=creds.authorize(Http()))
        self.service = service

    def create(self, title):
        service = self.service
        # [START sheets_create]
        spreadsheet = {
            'properties': {
                'title': title
            }
        }
        spreadsheet = service.spreadsheets().create(body=spreadsheet,
                                            fields='spreadsheetId').execute()
        print('Spreadsheet ID: {0}'.format(spreadsheet.get('spreadsheetId')))
        # [END sheets_create]
        return spreadsheet.get('spreadsheetId')

    def get_values(self, spreadsheet_id, range_name='A:ZZZ', majorDimension='ROWS'):
        service = self.service
        # [START sheets_get_values]
        result = service.spreadsheets().values().get(
            spreadsheetId=spreadsheet_id, range=range_name, majorDimension=majorDimension).execute()

        # For example, if the spreadsheet data is: `A1=1,B1=2,A2=3,B2=4`,
        # then requesting `range=A1:B2,majorDimension=ROWS` will return
        # `[[1,2],[3,4]]`,
        # whereas requesting `range=A1:B2,majorDimension=COLUMNS` will return

        #numRows = len(result.get('values')) if result.get('values')is not None else 0
        #print('{0} rows retrieved.'.format(numRows))
        # [END sheets_get_values]
        return result

    def update_values(self, spreadsheet_id, range_name, values,majorDimension='ROWS',value_input_option='RAW'):
        service = self.service

        #value_input_options
        # INPUT_VALUE_OPTION_UNSPECIFIED	Default input value. This value must not be used.
        # RAW	The values the user has entered will not be parsed and will be stored as-is.
        # USER_ENTERED	The values will be parsed as if the user typed them into the UI. Numbers will stay as numbers, but strings may be converted to numbers, dates, etc. following the same rules that are applied when entering text into a cell via the Google Sheets UI.
        body = {
            'values': values,
            'majorDimension': majorDimension
        }
        result = service.spreadsheets().values().update(
            spreadsheetId=spreadsheet_id, range=range_name,
            valueInputOption=value_input_option, body=body).execute()
        print('{0} cells updated.'.format(result.get('updatedCells')))
        return result

    def append_values(self, spreadsheet_id, range_name, values,majorDimension='ROWS',value_input_option='RAW'):
        service = self.service
        body = {
            'values': values,
            'majorDimension': majorDimension
        }
        result = service.spreadsheets().values().append(
            spreadsheetId=spreadsheet_id, range=range_name,
            valueInputOption=value_input_option, body=body).execute()
        print('{0} cells appended.'.format(result \
                                               .get('updates') \
                                               .get('updatedCells')))
        # [END sheets_append_values]
        return result

    def batch_update(self, spreadsheet_id, title, find, replacement):
        service = self.service
        # [START sheets_batch_update]
        requests = []
        # Change the spreadsheet's title.
        requests.append({
            'updateSpreadsheetProperties': {
                'properties': {
                    'title': title
                },
                'fields': 'title'
            }
        })
        # Find and replace text
        requests.append({
            'findReplace': {
                'find': find,
                'replacement': replacement,
                'allSheets': True
            }
        })
        # Add additional requests (operations) ...

        body = {
            'requests': requests
        }
        response = service.spreadsheets().batchUpdate(
            spreadsheetId=spreadsheet_id,
            body=body).execute()
        find_replace_response = response.get('replies')[1].get('findReplace')
        print('{0} replacements made.'.format(
            find_replace_response.get('occurrencesChanged')))
        # [END sheets_batch_update]
        return response

    def batch_get_values(self, spreadsheet_id, _range_names):
        service = self.service
        # [START sheets_batch_get_values]
        range_names = [
            # Range names ...
        ]
        # [START_EXCLUDE silent]
        range_names = _range_names
        # [END_EXCLUDE]
        result = service.spreadsheets().values().batchGet(
            spreadsheetId=spreadsheet_id, ranges=range_names).execute()
        print('{0} ranges retrieved.'.format(result.get('valueRanges')))
        # [END sheets_batch_get_values]
        return result

    def batch_update_values(self, spreadsheet_id, range_name,
                            value_input_option, _values):
        service = self.service
        # [START sheets_batch_update_values]
        values = [
            [
                # Cell values ...
            ],
            # Additional rows
        ]
        # [START_EXCLUDE silent]
        values = _values
        # [END_EXCLUDE]
        data = [
            {
                'range': range_name,
                'values': values
            },
            # Additional ranges to update ...
        ]
        body = {
            'valueInputOption': value_input_option,
            'data': data
        }
        result = service.spreadsheets().values().batchUpdate(
            spreadsheetId=spreadsheet_id, body=body).execute()
        print('{0} cells updated.'.format(result.get('updatedCells')))
        # [END sheets_batch_update_values]
        return result

    def pivot_tables(self, spreadsheet_id):
        service = self.service
        # Create two sheets for our pivot table.
        body = {
            'requests': [{
                'addSheet': {}
            }, {
                'addSheet': {}
            }]
        }
        batch_update_response = service.spreadsheets() \
            .batchUpdate(spreadsheetId=spreadsheet_id, body=body).execute()
        source_sheet_id = batch_update_response.get('replies')[0] \
            .get('addSheet').get('properties').get('sheetId')
        target_sheet_id = batch_update_response.get('replies')[1] \
            .get('addSheet').get('properties').get('sheetId')
        requests = []
        # [START sheets_pivot_tables]
        requests.append({
            'updateCells': {
                'rows': {
                    'values': [
                        {
                            'pivotTable': {
                                'source': {
                                    'sheetId': source_sheet_id,
                                    'startRowIndex': 0,
                                    'startColumnIndex': 0,
                                    'endRowIndex': 101,
                                    'endColumnIndex': 8
                                },
                                'rows': [
                                    {
                                        'sourceColumnOffset': 6,
                                        'showTotals': True,
                                        'sortOrder': 'ASCENDING',

                                    },

                                ],
                                'columns': [
                                    {
                                        'sourceColumnOffset': 3,
                                        'sortOrder': 'ASCENDING',
                                        'showTotals': True,

                                    }
                                ],
                                'values': [
                                    {
                                        'summarizeFunction': 'COUNTA',
                                        'sourceColumnOffset': 3
                                    }
                                ],
                                'valueLayout': 'HORIZONTAL'
                            }
                        }
                    ]
                },
                'start': {
                    'sheetId': target_sheet_id,
                    'rowIndex': 0,
                    'columnIndex': 0
                },
                'fields': 'pivotTable'
            }
        })
        body = {
            'requests': requests
        }
        response = service.spreadsheets() \
            .batchUpdate(spreadsheetId=spreadsheet_id, body=body).execute()
        # [END sheets_pivot_tables]
        return response

    def conditional_formatting(self, spreadsheet_id):
        service = self.service

        # [START sheets_conditional_formatting]
        my_range = {
            'sheetId': 0,
            'startRowIndex': 1,
            'endRowIndex': 11,
            'startColumnIndex': 0,
            'endColumnIndex': 4,
        }
        requests = [{
            'addConditionalFormatRule': {
                'rule': {
                    'ranges': [my_range],
                    'booleanRule': {
                        'condition': {
                            'type': 'CUSTOM_FORMULA',
                            'values': [{
                                'userEnteredValue':
                                    '=GT($D2,median($D$2:$D$11))'
                            }]
                        },
                        'format': {
                            'textFormat': {
                                'foregroundColor': {'red': 0.8}
                            }
                        }
                    }
                },
                'index': 0
            }
        }, {
            'addConditionalFormatRule': {
                'rule': {
                    'ranges': [my_range],
                    'booleanRule': {
                        'condition': {
                            'type': 'CUSTOM_FORMULA',
                            'values': [{
                                'userEnteredValue':
                                    '=LT($D2,median($D$2:$D$11))'
                            }]
                        },
                        'format': {
                            'backgroundColor': {
                                'red': 1,
                                'green': 0.4,
                                'blue': 0.4
                            }
                        }
                    }
                },
                'index': 0
            }
        }]
        body = {
            'requests': requests
        }
        response = service.spreadsheets() \
            .batchUpdate(spreadsheetId=spreadsheet_id, body=body).execute()
        print('{0} cells updated.'.format(len(response.get('replies'))))
        # [END sheets_conditional_formatting]
        return response
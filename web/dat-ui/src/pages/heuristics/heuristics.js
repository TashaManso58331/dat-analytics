import React from 'react';
import DataGrid, {
  Column,
  Pager,
  Paging,
  FilterRow,
  Lookup,
  Sorting
} from 'devextreme-react/data-grid';

const columns = ['Origin', 'DHO', 'Destination', 'Trip', 'Weight', 'Rate', 'W_CostOfMile', 'W_State', 'W_Rate', 'W_DHO', 'W_Summary', 'Company'];

export default function Task() {
  return (
    <React.Fragment>
      <h2 className={'content-block'}>Heuristics</h2>
    <DataGrid
              dataSource="https://datanalyticsengine.azurewebsites.net/datfetch"
              defaultColumns={columns}
              showBorders={true}
    >
        <Sorting mode="multiple" />
    </DataGrid>
    </React.Fragment>
)}

import React, { Component } from 'react';
import { Table } from 'reactstrap';

class RestockDataTable extends Component {
    render() {
        const items = this.props.items;
        return <Table striped>
          <thead className="thead-dark">
            <tr>
              <th>Date</th>
              <th>Item</th>
              <th>Quantity</th>
              <th>Manufacturer</th>
              <th>Whole-sale</th>
            </tr>
          </thead>
          <tbody>
            {!items || items.length <= 0 ?
              <tr>
                <td colSpan="6" align="center"><b>No restock info yet</b></td>
              </tr>
              : items.map(item => (
                <tr>
                  <th>
                    {item.restock_date}
                  </th>
                  <td>
                    {item.item_stocked}
                  </td>
                  <td>
                    {item.item_quantity}
                  </td>
                  <td>
                    {item.manufacturer}
                  </td>
                  <td>
                    {item.wholesale_price}
                  </td>
                </tr>
              ))}
          </tbody>
        </Table>;
      }
    }
export default RestockDataTable;
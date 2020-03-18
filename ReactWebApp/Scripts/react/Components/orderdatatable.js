import React, { Component } from 'react';
import { Table } from 'reactstrap';

class OrderDataTable extends Component {
    render() {
        const items = this.props.items;
        return <Table striped>
          <thead className="thead-dark">
            <tr>
              <th>Id</th>
              <th>Customer</th>
              <th>Date</th>
              <th>Item</th>
              <th>Quantity</th>
              <th>Price</th>
            </tr>
          </thead>
          <tbody>
            {!items || items.length <= 0 ?
              <tr>
                <td colSpan="6" align="center"><b>No order info yet</b></td>
              </tr>
              : items.map(item => (
                <tr key={item.order_id}>
                  <th scope="row">
                    {item.order_id}
                  </th>
                  <td>
                    {item.customer_id}
                  </td>
                  <td>
                    {item.order_date}
                  </td>
                  <td>
                    {item.item_ordered}
                  </td>
                  <td>
                    {item.item_quantity}
                  </td>
                  <td>
                    {item.item_price}
                  </td>
                </tr>
              ))}
          </tbody>
        </Table>;
      }
    }
export default OrderDataTable;
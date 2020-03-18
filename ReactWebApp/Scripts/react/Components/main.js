import React, { Component, useState } from 'react';
import OrderDataTable from './orderdatatable';
import RestockDataTable from './restockdatatable';
import orderJson from './orders.json';
import restockJson from './restocks.json';
import Button from "./button";
import apiCall from "./apiCall";
import {appStyle} from './styles'


class Main extends Component {
    constructor() {
        super();
        this.state = {
            orderItems: [],
            restockItems: [],
            success: null
        };
        this.state.orderItems = orderJson.sort((a, b) => a.order_id > b.order_id);
        this.state.restockItems = restockJson;
    }
    
    render() {
        return (
            <div style={appStyle} >
        <Button text={this.state.success ? "SUCCESS" : this.state.success == null ? "RUN" : "FAIL"} onClick={() => apiCall(this.state.orderItems, this.state.restockItems, this)} />

        <h1>Orders</h1>
        <OrderDataTable items={this.state.orderItems} />
        <h2>Restocks</h2>
        <RestockDataTable items={this.state.restockItems} /></div>
    );
    }  
}

export default Main;
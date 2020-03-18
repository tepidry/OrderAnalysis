import React, { Component, useState } from 'react';
import Tab from 'react-bootstrap/Tab'
import Tabs from 'react-bootstrap/Tabs'
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
            restockItems: []    
        };
        this.state.orderItems = orderJson.sort((a, b) => a.order_id > b.order_id);
        this.state.restockItems = restockJson;
    }

    // componentDidMount() {
//     this.getItems(); 
    // }
    // getItems(){
    //     fetch(API_URL)
    //         .then(res => res.json())
    //         .then(res => this.setState({ items: res }))
    //         .catch(err => console.log(err));
    // }
    // addUserToState(user){
    //     this.setState(previous => ({
    //         items: [...previous.items, user]    
    //     }));
    // }
    // updateState(id){
    //     this.getItems();
    // }
    // deleteItemFromState(id){
    //     const updated = this.state.items.filter(item => item.id !== id);
    //     this.setState({ items: updated })
    // }
        render() {
            return (
                <div style={appStyle}>
                    <Tabs defaultActiveKey="profile" id="uncontrolled-tab-example">
                        <Tab eventKey="home" title="Home">
                            <OrderDataTable items={this.state.orderItems} />
                        </Tab>
                        <sp/>
                        <Tab eventKey="profile" title="Profile">
                            <RestockDataTable items={this.state.restockItems} />
                        </Tab>
                    </Tabs>
                 </div>);
        //<div style={appStyle} >
        //    <Button text="Run" onClick={() => apiCall(this.state.orderItems, this.state.restockItems)} />
        //    <Tabs >
        //        <TabList>
        //            <Tab>
        //                <h1>Orders</h1>
        //            </Tab>
        //            <Tab><h1>Restock</h1></Tab>
        //        </TabList>
            
        //        <TabPanel >
        //            <OrderDataTable items={this.state.orderItems} />
        //        </TabPanel>
        //        <TabPanel>
        //            <RestockDataTable items={this.state.restockItems} />
        //        </TabPanel>
        //    </Tabs></div>
            
        
        //);
    }  
         
}

export default Main;
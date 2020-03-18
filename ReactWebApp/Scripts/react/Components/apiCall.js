const API_URL = 'https://localhost:12345/api/order/test';


const apiCall = (orderData, restockData, button) =>
    new Promise((resolve, reject) => {
        fetch(`${API_URL}`,
            {
                method: 'post',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({
                    orders: orderData,
                    restocks: restockData
                })
            })
            .then((resp) => {
                if(!resp.ok)
                {
                    throw Error(resp.statusText);
                }
              return resp.json()
            }) 
            .then((data) => {
                button.setState(state => ({
                    success: data.success
                  }));
              resolve(data)                    
            })
            .catch((error) => {
              reject(error)
            })
        })
    

export default apiCall;
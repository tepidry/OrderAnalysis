import React from 'react';
import { appStyle } from "./styles";
import Button from "./button";
import apiCall from "./apiCall";

class FileGetter extends React.Component {
  constructor(props) {
    super(props);
    this.handleSubmit = this.handleSubmit.bind(this);
    this.orderFileInput = React.createRef();
  }
  handleSubmit(event) {
    event.preventDefault();
    alert(
      `Selected file - ${this.fileInput.current.files[0].name}`
    );
  }

  render() {
    return (
      <form style={appStyle} onSubmit={this.handleSubmit}>
        <label>
          Upload Order file:
          <input type="file" ref={this.orderFileInput} />
        </label>
        <br />
        {/* <label>
          Upload Restock file:
          <input type="file" ref={this.restockFileInput} />
        </label> */}
        <br />
        <br />
        <Button text="Run" onClick={() => apiCall(this.orderFileInput)} />
      </form>
    );
  }
}

export default FileGetter
// import React from 'react';
// import { buttonStyle } from "./styles";
// import ReactNestedLoader from "react-nested-loader";

// // Step1: define your button
// let runButton = ({
//     onClick,
//     text,
//     loading, // will be injected
//     error, // will be injected
// }) => (
//     <button onClick={onClick} style={buttonStyle(loading, error)}>
//         {error ? "ERROR" : loading ? "..." : text}
//     </button>
// );

// // Step2: wrap your button so that it receives the loading prop
// runButton = ReactNestedLoader({
//     onError: (error, remove) => setTimeout(remove, 1000),
// })(runButton);

// export default runButton;
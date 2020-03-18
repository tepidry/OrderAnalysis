import React from 'react';
import ReactNestedLoader from "react-nested-loader";
import { buttonStyle } from "./styles";

let Button = ({
    onClick,
    text,
    loading, // will be injected
    error, // will be injected
}) => (
<button onClick={onClick} style={buttonStyle(loading, error)}>
    {error ? "ERROR" : loading ? "..." : text}
    </button>
);

Button = ReactNestedLoader({
    onError: (error, remove) => setTimeout(remove, 1000),
})(Button);

export default Button;
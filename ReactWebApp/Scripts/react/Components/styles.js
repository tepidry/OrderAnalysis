export const appStyle = {
    width: "10vu",
    height: "10vh",
    display: "inline",
    justifyContent: "left",
};

export const buttonStyle = (loading, error) => ({
    outline: 0,
    textAlign: "center",
    padding: "2px 5px",
    margin: "2px 5px",
    fontSize: "10px",
    cursor: "pointer",
    backgroundColor: "lightgrey",
    width: 100,
    border: "solid 2px black",
    borderRadius: 10,
    transition: "all 150ms ease-in-out",
    borderColor: error ? "red" : loading ? "blue" : "black",
    color: error ? "red" : loading ? "blue" : "black",
    transform: error ? "scale(1.2)" : "scale(1.0)",
});
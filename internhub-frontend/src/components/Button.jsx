import React from "react";

export default function Button(props) {
  return (
    <button
      className={"btn btn-" + (props.buttonColor ?? "primary")}
      onClick={props.onClick}
      onSubmit={props.onSubmit}
      type={props.type ?? "button"}
    >
      {props.children}
    </button>
  );
}

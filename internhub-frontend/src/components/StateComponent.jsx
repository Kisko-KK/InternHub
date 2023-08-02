import React from "react";

export default function StateComponent({ state }) {
  return (
    <div
      style={{
        "margin-right": "50px",
        padding: 6,
        "border-radius": "8px",
        backgroundColor:
          state.toLowerCase() === "accepted"
            ? "green"
            : state.toLowerCase() === "declined"
            ? "red"
            : "orange",
      }}
    >
      {state}
    </div>
  );
}

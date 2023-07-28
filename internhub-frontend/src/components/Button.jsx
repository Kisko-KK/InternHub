import React from "react";

export default function Button({
  buttonColor,
  onClick,
  onSubmit,
  type,
  children,
}) {
  return (
    <button
      className={"btn btn-" + (buttonColor ?? "primary")}
      onClick={onClick}
      onSubmit={onSubmit}
      type={type ?? "button"}
    >
      {children}
    </button>
  );
}

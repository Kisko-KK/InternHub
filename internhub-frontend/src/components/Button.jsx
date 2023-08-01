import React from "react";

export default function Button({
  buttonColor,
  onClick,
  onSubmit,
  type,
  children,
  className,
}) {
  return (
    <button
      className={"btn btn-" + (buttonColor ?? "primary") + ` ${className}`}
      onClick={onClick}
      onSubmit={onSubmit}
      type={type ?? "button"}
    >
      {children}
    </button>
  );
}

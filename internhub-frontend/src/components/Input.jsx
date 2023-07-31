import React, { useEffect } from "react";

export default function Input({
  name,
  text,
  defaultValue,
  value,
  onChange,
  type,
  required = false,
}) {
  return (
    <div className="mb-3 mt-3">
      <label htmlFor={name} className="form-label">
        {text}
      </label>
      <br></br>
      <input
        type={type ?? "text"}
        id={name}
        name={name}
        value={value}
        onChange={onChange}
        defaultValue={defaultValue}
        className="form-control"
        required={required}
      />
      <br></br>
    </div>
  );
}

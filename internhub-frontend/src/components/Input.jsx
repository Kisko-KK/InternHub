import React from "react";

export default function Input({ name, text, value, type }) {
  return (
    <div className="mb-3 mt-3 w-50">
      <label htmlFor={name} className="form-label text-light">
        {text}
      </label>
      <br></br>
      <input
        type={type ?? "text"}
        id={name}
        name={name}
        defaultValue={value}
        className="form-control"
        required
      />
      <br></br>
    </div>
  );
}

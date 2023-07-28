import React from "react";

export default function CheckBox({ name, text, checked, onChange }) {
  return (
    <div className="mb-3 mt-3 w-50">
      <input
        type="checkbox"
        name={name}
        className="form-check-input"
        checked={checked}
        onChange={() => onChange(!checked)}
      />
      <label htmlFor={name} className="form-check-label text-light">
        {text}
      </label>
    </div>
  );
}

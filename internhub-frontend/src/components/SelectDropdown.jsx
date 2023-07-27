import React from "react";

export default function SelectDropdown({
  name,
  list,
  placeholder,
  selectedId,
  text,
}) {
  return (
    <div className="mb-3 mt-3 w-50">
      <label htmlFor={name} className="form-label text-light">
        {text}
      </label>
      <br></br>
      <select name={name} id={name} required>
        <option value="" selected={selectedId ? false : true}>
          {placeholder}
        </option>
        {list.map((data) => {
          return (
            <option
              key={data.id}
              value={data.id}
              selected={selectedId === data.id}
            >
              {data.name}
            </option>
          );
        })}
      </select>
    </div>
  );
}

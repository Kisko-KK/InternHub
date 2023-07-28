import React, { useState } from "react";
import { CompanyFilter } from "../../models";
import Button from "../Button";
import Form from "../Form";
import Input from "../Input";

export default function CompanyFilterComponent({ onFilter, onClearFilter }) {
  const [isFilterActive, setIsFilterActive] = useState(false);
  return (
    <Form
      onSubmit={(e) => {
        e.preventDefault();
        const filter = new CompanyFilter({
          name: e.target.name.value,
        });
        onFilter(filter);
        setIsFilterActive(true);
      }}
    >
      <Input text="Name" name="name" />
      <Button type="submit" buttonColor="primary">
        Filter
      </Button>
      {isFilterActive && (
        <Button buttonColor="secondary" onClick={() => onClearFilter()}>
          Clear filter
        </Button>
      )}
    </Form>
  );
}

import React, { useState } from "react";
import { StudentFilter } from "../../models";
import Button from "../Button";
import CheckBox from "../CheckBox";
import Form from "../Form";
import Input from "../Input";

export default function StudentFilterComponent({ onFilter, onClearFilter }) {
  const [isActive, setIsActive] = useState(true);
  const [isFilterActive, setIsFilterActive] = useState(false);

  return (
    <Form
      onSubmit={(e) => {
        e.preventDefault();
        const filter = new StudentFilter({
          firstName: e.target.firstname.value,
          lastName: e.target.lastname.value,
          isActive: isActive,
        });
        onFilter(filter);
        setIsFilterActive(true);
      }}
    >
      <Input text="First name" name="firstname" />
      <Input text="Last name" name="lastname" />
      <CheckBox
        text="Active"
        name="isactive"
        checked={isActive}
        onChange={(value) => {
          setIsActive(value);
        }}
      />
      <Button type="submit" buttonColor="primary">
        Filter
      </Button>
      {isFilterActive && (
        <Button
          buttonColor="secondary"
          onClick={() => {
            onClearFilter();
          }}
        >
          Clear filter
        </Button>
      )}
    </Form>
  );
}

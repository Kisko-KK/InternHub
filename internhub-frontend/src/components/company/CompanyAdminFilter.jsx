import { useState } from "react";
import { CompanyFilter } from "../../models";
import Button from "../Button";
import CheckBox from "../CheckBox";
import Form from "../Form";
import Input from "../Input";

export default function CompanyAdminFilter({ onFilter, onClearFilter }) {
  const [isActive, setIsActive] = useState(true);
  const [isAccepted, setIsAccepted] = useState(false);
  const [isFilterActive, setIsFilterActive] = useState(false);

  return (
    <Form
      onSubmit={(e) => {
        e.preventDefault();
        const filter = new CompanyFilter({
          name: e.target.name.value,
          isActive: isActive,
          isAccepted: isAccepted,
        });
        onFilter(filter);
        setIsFilterActive(true);
      }}
    >
      <Input text="Name" name="name" />
      <CheckBox
        text="Active"
        name="isactive"
        checked={isActive}
        onChange={(value) => {
          setIsActive(value);
        }}
      />
      <CheckBox
        text="Accepted"
        name="isaccepted"
        checked={isAccepted}
        onChange={(value) => {
          setIsAccepted(value);
        }}
      />
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

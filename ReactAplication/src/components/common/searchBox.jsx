import React from "react";

const SearchBox = ({ value, onChange, ...rest }) => {
  return (
    <input
      type="text"
      name="query"
      className="form-control my-3"
      placeholder="search..."
      value={value}
      onChange={(e) => onChange(e.currentTarget.value)}
      {...rest}
    ></input>
  );
};

export default SearchBox;

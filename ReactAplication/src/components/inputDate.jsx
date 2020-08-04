import React from "react";
import Moment from "moment";
const InputDate = ({ name, label, error, value, ...rest }) => {
  const format = "YYYY-MM-DD";

  return (
    <div className="form-group">
      <label htmlFor={name}>{label}</label>
      <input
        {...rest}
        className="form-control"
        name={name}
        type="date"
        value={Moment(value).format(format)}
      />

      {error && <div className="alert alert-danger">{error}</div>}
    </div>
  );
};

export default InputDate;

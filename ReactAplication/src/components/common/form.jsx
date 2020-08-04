import React, { Component } from "react";
import Joi from "joi-browser";
import Input from "./Input";
import InputDate from "../inputDate";

import Select from "./select";
import ActorSelect from "../actorSelect";
class Form extends Component {
  state = { data: {}, errors: {} };

  validateProperty = ({ value, name }) => {
    const property = { [name]: value };
    const schema = { [name]: this.schema[name] };
    const { error } = Joi.validate(property, schema);
    return error ? error.details[0].message : null;
  };

  validate = () => {
    const options = { abortEarly: false };

    const { error } = Joi.validate(this.state.data, this.schema, options);

    if (!error) return null;
    /* const errors = {};
        for (let error of result.error.details)
          errors[error.path[0]] = error.message; */

    const errors = error.details.reduce((previous, current) => {
      previous[current.path[0]] = current.message;
      return previous;
    }, {});

    return errors;
  };

  handleChange = ({ currentTarget: input }) => {
    const errors = { ...this.state.errors };
    const errorMessage = this.validateProperty(input);
    if (errorMessage) errors[input.name] = errorMessage;
    else delete errors[input.name];
    const data = { ...this.state.data };
    data[input.name] = input.value;
    this.setState({ data, errors });
  };

  handleActorChange = ({ currentTarget: input }) => {
    const errors = { ...this.state.errors };
    const data = { ...this.state.data };
    data["actors"].push({ name: "", id: input.value });
    let actor = input.value;

    console.log(actor);
    console.log(data["actors"]);

    this.setState({ data, errors, actor });
  };

  handleSubmit = async (e) => {
    e.preventDefault();
    const errors = this.validate();
    this.setState({ errors: errors || {} });

    if (errors) {
      console.log(errors);
      return;
    }

    await this.doSubmit();
  };

  renderButton = (label) => {
    return (
      <button className="btn btn-primary" disabled={this.validate()}>
        {label}
      </button>
    );
  };
  renderSelect(name, label, options) {
    const { data, errors } = this.state;

    return (
      <Select
        name={name}
        value={data[name]}
        label={label}
        options={options}
        onChange={this.handleChange}
        error={errors[name]}
      />
    );
  }

  renderActorSelect(name, label, options) {
    const { data, errors } = this.state;

    return (
      <ActorSelect
        name={this.state.actor}
        value={this.state.actor}
        label={label}
        options={options}
        onChange={this.handleActorChange}
        error={errors[name]}
      />
    );
  }

  renderInput(name, label, type = "text") {
    const { data, errors } = this.state;
    return (
      <Input
        type={type}
        value={data[name]}
        name={name}
        onChange={this.handleChange}
        label={label}
        error={errors[name]}
      />
    );
  }

  renderInputDate(name, label, type = "date") {
    const { data, errors } = this.state;
    return (
      <InputDate
        type={type}
        value={data[name]}
        name={name}
        onChange={this.handleChange}
        label={label}
        error={errors[name]}
      />
    );
  }
}

export default Form;

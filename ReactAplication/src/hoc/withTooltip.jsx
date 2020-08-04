import React, { Component } from "react";

export function WithTooltip(Component) {
  return class WithTooltip extends Reac.Component {
    render() {
      return (
        <div>
          <Component />
        </div>
      );
    }
  };
}

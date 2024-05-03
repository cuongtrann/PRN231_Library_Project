import React from "react";
import ReactDOM from "react-dom/client";
import "./index.css";
import { App } from "./App";
import { BrowserRouter } from "react-router-dom";
import { loadStripe } from "@stripe/stripe-js";
import { Elements } from "@stripe/react-stripe-js";

const stripePromies = loadStripe(
  "pk_test_51OBzHqFBNGCgVxxOil5yWu1KNGNKZEYQQwgK3gssSR8C2yhxIlENUu0dTchEowX3cYbzping2JGOf1163oatWYIV00VO7xgVzM"
);

const root = ReactDOM.createRoot(
  document.getElementById("root") as HTMLElement
);
root.render(
  <BrowserRouter>
    <Elements stripe={stripePromies}>
      <App />
    </Elements>
  </BrowserRouter>
);

import React from "react";
import ReactDOM from "react-dom/client";
import "./styles/index.css";
import reportWebVitals from "./reportWebVitals";
import { createBrowserRouter, RouterProvider } from "react-router-dom";
import {
  HomePage,
  LandingPage,
  CompanyRegisterPage,
  CompanyHomePage,
  LoginPage,
  StudentDetailsPage,
  StudentEditPage,
  StudentRegisterPage,
} from "./pages";

const router = createBrowserRouter([
  {
    path: "/",
    element: <LandingPage />,
  },
  {
    path: "/login",
    element: <LoginPage />,
  },
  {
    path: "/student/register",
    element: <StudentRegisterPage />,
  },
  {
    path: "/student/edit/:id",
    element: <StudentEditPage />,
  },
  {
    path: "/student/details/:id",
    element: <StudentDetailsPage />,
  },
  {
    path: "/company/register",
    element: <CompanyRegisterPage/>
  },
  {
    path: "/company/homepage",
    element: <CompanyHomePage/>
  }
]);

const root = ReactDOM.createRoot(document.getElementById("root"));
root.render(
  <React.StrictMode>
    <RouterProvider router={router} />
  </React.StrictMode>
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();

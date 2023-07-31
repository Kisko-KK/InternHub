import React from "react";
import ReactDOM from "react-dom/client";
import "./styles/index.css";
import reportWebVitals from "./reportWebVitals";
import { createBrowserRouter, RouterProvider } from "react-router-dom";
import {
  AdminCompaniesPage,
  AdminStudentsPage,
  HomePage,
  LandingPage,
  CompanyRegisterPage,
  CompanyHomePage,
  LoginPage,
  StudentDetailsPage,
  StudentEditPage,
  CompanyCreateInternship,
  StudentRegisterPage,
  CompanyProfilePage,
  CompanyEditPage,
  RequireAuthPage,
} from "./pages";
import { Company } from "./models";

const router = createBrowserRouter([
  {
    path: "/",
    element: <HomePage />,
  },
  {
    path: "/login",
    element: <LoginPage />,
  },
  {
    path: "/students",
    element: <RequireAuthPage roles={["Admin"]} page={<AdminStudentsPage />} />,
  },
  {
    path: "/companies",
    element: (
      <RequireAuthPage roles={["Admin"]} page={<AdminCompaniesPage />} />
    ),
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
    element: <CompanyRegisterPage />,
  },
  {
    path: "/company/profile/:id",
    element: <CompanyProfilePage />,
  },
  {
    path: "/company/edit/:id",
    element: <CompanyEditPage />,
  },
  {
    path: "/company",
    element: <CompanyHomePage />,
  },
  {
    path: "company/addnewinternship",
    element: <CompanyCreateInternship />,
  },
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

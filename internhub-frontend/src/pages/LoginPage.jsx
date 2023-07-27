import React from "react";
import { useNavigate } from "react-router-dom";
import { Button, Input } from "../components";
import { LoginService } from "../services";

export default function LoginPage() {
  const loginService = new LoginService();
  const navigate = useNavigate();

  async function onSubmit(e) {
    e.preventDefault();
    const result = await loginService.loginAsync({
      username: e.target.email.value,
      password: e.target.password.value,
    });
    if (result) navigate("/");
  }

  return (
    <div className="bg-dark">
      <h1 className="text-light">Login Page</h1>
      <form className="form" onSubmit={onSubmit}>
        <div className="mb-3 mt-3 w-50">
          <Input name="email" text="Email:" />
        </div>
        <div className="mb-3 mt-3 w-50">
          <Input type="password" name="password" text="Password:" />
        </div>
        <Button type={"submit"} buttonColor={"primary"}>
          Login
        </Button>
      </form>
    </div>
  );
}

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
    <div className="vh-100 d-flex justify-content-center align-items-center">
      <div className="container">
        <div className="row justify-content-center">
          <div className="col">
            <div className="text-center mb-3">
              <h1>Login</h1>
            </div>
            <form className="form" onSubmit={onSubmit}>
              <div className="mb-3">
                <Input name="email" text="Email:" />
              </div>
              <div className="mb-3">
                <Input type="password" name="password" text="Password:" />
              </div>
              <div className="text-center">
                <Button type={"submit"} buttonColor={"primary"}>
                  Login
                </Button>
              </div>
            </form>
          </div>
        </div>
      </div>
    </div>
  );
}

import axios from "axios";
import { HttpHeader, Server, UserToken } from "../models/index";

export class LoginService {
  async loginAsync({ username, password }) {
    try {
      const response = await axios.post(
        Server.host + "Login",
        { username, password, grant_type: "password" },
        {
          headers: {
            ...HttpHeader.get(),
            "Content-Type": "application/x-www-form-urlencoded",
          },
        }
      );
      if (response.status === 200) {
        const data = response.data;
        const userToken = UserToken.fromResponse(data);
        localStorage.setItem("user_token", JSON.stringify(userToken));
        return true;
      }
      return false;
    } catch {
      return false;
    }
  }

  logOut() {
    try {
      localStorage.removeItem("user_token");
      return true;
    } catch {
      return false;
    }
  }

  isUserTokenValid() {
    const userToken = this.getUserToken();
    if (!userToken) return false;
    return userToken.token && userToken.expires > Date.now();
  }

  getUserToken() {
    const data = JSON.parse(localStorage.getItem("user_token"));
    if (!data) return null;
    const userToken = new UserToken(data);
    return userToken;
  }
}

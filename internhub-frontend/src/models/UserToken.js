export class UserToken {
  constructor(data) {
    this.id = data.userId;
    this.token = data.token;
    this.fullName = data.fullName;
    this.email = data.email;
    this.expires = new Date(data.expires);
    this.role = data.role;
  }

  static fromResponse(json) {
    return new UserToken({
      userId: json.userId,
      token: json.access_token,
      fullName: json.fullName,
      email: json.email,
      expires: json[".expires"],
      role: json.role,
    });
  }
}

import { User } from "./User";
import { County } from "./County";

export class Company extends User {
  constructor({ name = "", website = "", ...user }) {
    super(user);
    this.name = name;
    this.website = website;
  }

  static fromJson(json) {
    return new Company({
      id: json["Id"],
      name: json["Name"],
      website: json["Website"],
      firstName: json["FirstName"],
      lastName: json["LastName"],
      address: json["Address"],
      description: json["Description"],
      countyId: json["County"] ? json["County"]["Id"] : null,
      county: County.fromJson(json["County"]),
      phoneNumber: json["PhoneNumber"],
      email: json["Email"],
      password: json["Password"],
    });
  }
}

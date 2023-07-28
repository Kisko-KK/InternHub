export class Company {
  constructor({ id = "", website = "", address = "", name = "" }) {
    this.id = id;
    this.website = website;
    this.address = address;
    this.name = name;
  }

  static fromJson(json) {
    return new Company({
      id: json["Id"],
      website: json["Website"],
      address: json["Address"],
      name: json["Name"],
    });
  }
}

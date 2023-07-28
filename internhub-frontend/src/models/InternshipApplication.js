import { Student, State } from "./index";

export class InternshipApplication {
  constructor({
    dateCreated = null,
    dateUpdated = null,
    message = "",
    student = null,
    state = null,
  }) {
    this.dateCreated = dateCreated;
    this.dateUpdated = dateUpdated;
    this.message = message;
    this.student = student;
    this.state = state;
  }

  static fromJson(json) {
    return new InternshipApplication({
      dateCreated: json["DateCreated"],
      dateUpdated: json["DateUpdated"],
      message: json["Message"],
      student: Student.fromJson(json["Student"]),
      state: State.fromJson(json["State"]),
    });
  }
}

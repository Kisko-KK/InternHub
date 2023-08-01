import { Student, State, Internship } from "./index";

export class InternshipApplication {
  constructor({
    dateCreated = null,
    dateUpdated = null,
    message = "",
    student = null,
    state = null,
    internship = null,
  }) {
    this.dateCreated = dateCreated;
    this.dateUpdated = dateUpdated;
    this.message = message;
    this.student = student;
    this.state = state;
    this.internship = internship;
  }

  static fromJson(json) {
    return new InternshipApplication({
      dateCreated: json["DateCreated"],
      dateUpdated: json["DateUpdated"],
      message: json["Message"],
      student: Student.fromJson(json["Student"]),
      state: State.fromJson(json["State"]),
      internship: Internship.fromJson(json["Internship"]),
    });
  }
}

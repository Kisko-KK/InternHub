import axios from "axios";
import { Server } from "../models";
import { HttpHeader } from "../models/HttpHeader";
import { PagedList } from "../models/PagedList";
import { Student } from "../models/Student";

const urlPrefix = Server.url + "Student";

export class StudentService {
  async getAsync(pageNumber) {
    try {
      const response = await axios.get(
        urlPrefix + `?CurrentPage=${pageNumber}&pageSize=5`,
        {
          headers: HttpHeader.get(),
        }
      );
      if (response.status !== 200) return [];
      const dataList = response.data["Data"].map((data) =>
        Student.fromJson(data)
      );
      const pagedList = PagedList.fromJson(response.data, dataList);
      return pagedList;
    } catch {
      return new PagedList({});
    }
  }

  async getByIdAsync(id) {
    const response = await axios.get(urlPrefix + "?id=" + id, {
      headers: HttpHeader.get(),
    });
    if (response.status !== 200) return null;
    return Student.fromJson(response.data);
  }

  async postAsync(student) {
    const response = await axios.post(urlPrefix, student, {
      headers: HttpHeader.get(),
    });
    return response === 200;
  }

  async updateAsync(id, student) {
    const response = await axios.put(urlPrefix + "/" + id, student, {
      headers: HttpHeader.get(),
    });
    return response === 200;
  }

  async removeAsync(id) {
    const response = await axios.delete(urlPrefix + "/" + id, {
      headers: HttpHeader.get(),
    });
    return response === 200;
  }
}

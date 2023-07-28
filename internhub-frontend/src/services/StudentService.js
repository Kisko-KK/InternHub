import axios from "axios";
import { PagedList, Server, Student } from "../models";
import { HttpHeader } from "../models";

const urlPrefix = Server.url + "Student";

export class StudentService {
  getUrlQuery({ pageNumber = 1, pageSize = 10, ...filter }) {
    const counties = filter.counties
      ? "&" + filter.counties.map((county) => `counties=${county}`).join("&")
      : "";
    const studyAreas = filter.studyAreas
      ? "&" +
        filter.studyAreas
          .map((studyArea) => `studyAreas=${studyArea}`)
          .join("&")
      : "";
    const urlQuery = `?CurrentPage=${pageNumber}&pageSize=${pageSize}&isActive=${
      filter.isActive
    }&firstName=${filter.firstName || ""}&lastName=${
      filter.lastName || ""
    }${counties}${studyAreas}`;
    return urlQuery;
  }

  async getAsync(params) {
    try {
      const response = await axios.get(urlPrefix + this.getUrlQuery(params), {
        headers: HttpHeader.get(),
      });
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

  async getAdminAsync(params) {
    try {
      const response = await axios.get(
        urlPrefix + "/admin" + this.getUrlQuery(params),
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
    try {
      const response = await axios.get(urlPrefix + "?id=" + id, {
        headers: HttpHeader.get(),
      });
      if (response.status !== 200) return null;
      return Student.fromJson(response.data);
    } catch {
      return null;
    }
  }

  async postAsync(student) {
    try {
      const response = await axios.post(urlPrefix, student, {
        headers: HttpHeader.get(),
      });
      return response === 200;
    } catch {
      return false;
    }
  }

  async updateAsync(id, student) {
    try {
      const response = await axios.put(urlPrefix + "/" + id, student, {
        headers: HttpHeader.get(),
      });
      return response === 200;
    } catch {
      return false;
    }
  }

  async removeAsync(id) {
    try {
      const response = await axios.delete(urlPrefix + "/" + id, {
        headers: HttpHeader.get(),
      });
      return response === 200;
    } catch {
      return false;
    }
  }
}

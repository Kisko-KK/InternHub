import axios from "axios";
import {
  InternshipApplication,
  HttpHeader,
  PagedList,
  Server,
} from "../models";

const urlPrefix = Server.url + "InternshipApplication";

export class InternshipApplicationService {
  async getAsync({ pageNumber, pageSize, ...filter }) {
    try {
      const states =
        filter.states && filter.states.length > 0
          ? "&" + filter.states.map((state) => `states=${state}`).join("&")
          : "";
      const response = await axios.get(
        urlPrefix +
          `?CurrentPage=${pageNumber}&pageSize=${pageSize}&studentId=${
            filter.studentId || ""
          }&companyName=${filter.companyName || ""}&internshipName=${
            filter.internshipName || ""
          }${states}`,
        {
          headers: HttpHeader.get(),
        }
      );
      if (response.status !== 200) return [];
      const dataList = response.data["Data"].map((data) =>
        InternshipApplication.fromJson(data)
      );
      console.log(dataList);
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
      return InternshipApplication.fromJson(response.data);
    } catch {
      return null;
    }
  }

  async postAsync(internshipId, applyMessage) {
    try {
      const response = await axios.post(urlPrefix, {"InternshipId" : internshipId, "Message" : applyMessage}, {
        headers: HttpHeader.get(),
      });
      return response.status === 200;
    } catch {
      return false;
    }
  }

  async deleteAsync(InternshipApplicationId) {
    try{
      const response = await axios.delete(urlPrefix + `/${InternshipApplicationId}`, {headers: HttpHeader.get()});
      return response.status === 200;
    }catch{
      return false;
    }
  }

  async getIdAsync(studentId, internshipId){
    try{
      const response = await axios.get(urlPrefix + `/GetId?studentId=${studentId}&internshipId=${internshipId}`, {headers: HttpHeader.get()});
      if(response.status === 200){
        return response.data;
      }
      return null;
    }catch{
      return null;
    }
  }

}

import axios from "axios";
import { Internship, HttpHeader, Server } from "../models";
import { PagedList } from "../models/PagedList";

const urlPrefix = Server.url + "Internship";

export class InternshipService{
    async getAsync({pageNumber, pageSize, ...filterData }) {
        try {
          const counties = filterData.counties ? filterData.counties.map((county) => "&Counties=" + county).join("") : "";

            const response = await axios.get(
              urlPrefix + `?CurrentPage=${pageNumber}&pageSize=${pageSize}&Name=${filterData.name || ""}&startDate=${filterData.startDate}&endDate=${filterData.endDate}${counties}`,
              {
                headers: HttpHeader.get(),
              }
            );
            if (response.status !== 200) return [];
            console.log("ovo" + response);
            const dataList = response.data["Data"].map((data) =>
              Internship.fromJson(data)
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
        return Internship.fromJson(response.data);
      } catch {
        return null;
      }
    }

    async getIsStudentRegisteredToInternshipAsync(studentId, internshipId) {
      try {
        const response = await axios.get(urlPrefix + "/IsStudentRegisteredToInternship" + "?studentId=" + studentId + "&internshipId=" + internshipId, {
          headers: HttpHeader.get(),
        });
        console.log(response);
        return response.data;
      } catch {
        return null;
      }
    }

    convertToShorterDate = (fullDate) =>{
      const date = new Date(fullDate);
      return `${date.getFullYear()}-${(date.getMonth() + 1).toString().padStart(2, "0")}-${date.getDate().toString().padStart(2, "0")}`;
  }
}
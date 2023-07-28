import axios from "axios";
import { Internship, HttpHeader, Server } from "../models";
import { PagedList } from "../models/PagedList";

const urlPrefix = Server.url + "Internship";

export class InternshipService{
    async getAsync(pageNumber, filterData ) {

        try {
          let filterCountiesQuery = "";
          const counties = filterData.counties ? filterData.counties.map((county) => "&Counties=" + county).join() : "";


            const response = await axios.get(
              urlPrefix + `?CurrentPage=${pageNumber}&pageSize=3&Name=${filterData.name || ""}&startDate=${filterData.startDate}&endDate=${filterData.endDate}${counties}`,
              {
                headers: HttpHeader.get(),
              }
            );
            if (response.status !== 200) return [];
            console.log(response);
            const dataList = response.data["Data"].map((data) =>
              Internship.fromJson(data)
            );
            const pagedList = PagedList.fromJson(response.data, dataList);
            return pagedList;
           } catch {
             return new PagedList({});
           }
    } 
}
import React, { useEffect, useState } from 'react';
import Select from 'react-select';
import { CountyService } from '../services';

const InternshipFilter = ({  onFilter, onCancel }) => {

  const [counties, setCounties] = useState([]);
  const [startDate, setStartDate] = useState('');
  const [endDate, setEndDate] = useState('');
  const [name, setName] = useState('');
  const [countiesOptions, setCountiesOptions] = useState([]);

  const countyService = new CountyService();

  const handleFilter = () => {
    const filterData = {
      counties: counties.map((county) => county.value), 
      startDate: startDate || "",
      endDate: endDate || "",
      name: name || "",
    };
    onFilter(filterData);
  };

  const handleCancel = () =>{
    setCounties([]);
    setEndDate("");
    setStartDate("");
    setName("");
    onCancel();
  }

  const fetchCountiesAsync = async () =>{
    let counties = await countyService.getAsync();
    let mappedCounties = counties.map((county) => ({
      value: county.id,
      label: county.name
    }))
    setCountiesOptions(mappedCounties);
  }
  useEffect(() => {
    fetchCountiesAsync();
  }, [])



  return (
    <div className="filter-container d-flex justify-content-center align-items-center">
      <div className="row">
        <div className="col-md-3">
          <label>Name:</label>
          <input
            type="text"
            className="form-control"
            value={name}
            onChange={(e) => setName(e.target.value)}
          />
        </div>

        <div className="col-md-3">
          <label>Start Date:</label>
          <input
            type="date"
            className="form-control"
            value={startDate}
            onChange={(e) => setStartDate(e.target.value)}
          />
        </div>

        <div className="col-md-3">
          <label>End Date:</label>
          <input
            type="date"
            className="form-control"
            value={endDate}
            onChange={(e) => setEndDate(e.target.value)}
          />
        </div>

        <div className="col-md-3">
          <label>Counties:</label>
          <Select
            options={countiesOptions}
            isMulti
            value={counties}
            onChange={(selectedOptions) => setCounties(selectedOptions)}
          />
        </div>
      </div>

      <div className="mt-3">
        <button className="btn btn-primary" onClick={handleFilter}>
          Filter
        </button>
      </div>
      <div className='mt-3'>
        <button className="btn btn-secondary" onClick={handleCancel}>
          Cancel
        </button>
      </div>
    </div>
  );
};

export default InternshipFilter;

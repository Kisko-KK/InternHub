import React, { useState, useEffect } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import Form from '../Form';
import Button from '../Button';
import { InternshipService } from '../../services';
import StudentNavigation from '../student/StudentNavigation';
import Input from '../Input';
import { Internship } from '../../models';
import { NotFoundPage } from '../../pages';
import Loader from '../Loader';
import NavigationBar from '../NavigationBar';

const InternshipEdit = () => {
    const [internship, setInternship] = useState({});
    const { internshipId } = useParams();
    const [loading, setLoading] = useState(true);
    const [name, setName] = useState();
    const [startDate, setStartDate] = useState(new Date());
    const [endDate, setEndDate] = useState(new Date());
    const [description, setDescription] = useState();
    const [address, setAddress] = useState();
    const navigate = useNavigate();

    const internshipService = new InternshipService();

    const fetchInternship = async () => {
        const internship = await internshipService.getByIdAsync(internshipId);
        setInternship(internship);
        if (internship) {
            setName(internship.name);
            setStartDate(internshipService.convertToShorterDate(internship.startDate));
            setEndDate(internshipService.convertToShorterDate(internship.endDate));
            setDescription(internship.description);
            setAddress(internship.address);
        }
        setLoading(false);
    }

    useEffect(() => {
        fetchInternship();
    }, []);

    const handleNameChange = (e) => {
        setName(e.target.value);
    };

    const handleStartDate = (e) => {
        setStartDate(e.target.value);
    };
    const handleEndDate = (e) => {
        setEndDate(e.target.value);
    };

    const handleDescriptionChange = (e) => {
        setDescription(e.target.value);
    };

    const handleAddressChange = (e) => {
        setAddress(e.target.value);
    }

    const updateInternshipAsync = async (internshipId, newInternship) => {
        try {
            const isSuccess = await internshipService.updateAsync(internshipId, newInternship);
            if (isSuccess) {
                alert("Internship successfully updated!");
                navigate("/company");
            }
        }
        catch (error) {
            console.log("Failed to update internship!", error);
        }
    }

    const handleSubmit = async (e) => {
        e.preventDefault();

        const newInternship = new Internship(
            {
                name, startDate, endDate, description, address
            }
        );
        const result = await updateInternshipAsync(internshipId, newInternship)

        if (!result) {

        }
        else {
            return null;

        }
    }

    if (loading) return <Loader />;

    if (!internship) return <NotFoundPage />;

    return (
        <div className="container">
  <NavigationBar />
  <h1 className="text-center mt-5 title">Edit Internship</h1>

  <div className="row justify-content-center mt-3">
    <div className="col-md-8">
      <div className="card">
        <div className="card-body">
          <div className="mb-3">
            <Input
              text="Name: "
              type="text"
              id="name"
              className="form-control"
              value={name || ''}
              onChange={handleNameChange}
              required={false}
            />
          </div>
          <div className="mb-3">
            
            <Input
              text="Start date: "
              type="date"
              id="startDate"
              className="form-control"
              value={startDate || ''}
              onChange={handleStartDate}
              required={false}
            />
          </div>
          <div className="mb-3">
            <Input
              text="End date: "
              type="date"
              id="endDate"
              className="form-control"
              value={endDate || ''}
              onChange={handleEndDate}
              required={false}
            />
          </div>
          <div className="mb-3">
            <Input
              text="Description: "
              type="text"
              id="description"
              className="form-control"
              value={description || ''}
              onChange={handleDescriptionChange}
              required={false}
            />
          </div>
          <div className="mb-3">
            <Input
              text="Address: "
              type="text"
              id="address"
              className="form-control"
              value={internship.company?.address || ''}
              onChange={handleAddressChange}
              required={false}
            />
          </div>
          <div className="mb-3">
            <p className="text-center">
              <strong>Company:</strong> {internship.company ? internship.company.name : ''}
            </p>
            <p className="text-center">
              Check our{' '}
              <a href={internship.company ? internship.company.website : ''} target="_blank" rel="noopener noreferrer">
                website
              </a>{' '}
              for more details.
            </p>
          </div>

          <div className="d-flex justify-content-end">
            <Button className="btn btn-primary custom-button" type="submit">
              Submit
            </Button>
          </div>
        </div>
      </div>
    </div>
  </div>
</div>


    );


}
export default InternshipEdit;
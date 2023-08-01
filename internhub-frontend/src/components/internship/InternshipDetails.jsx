import React, { useState, useEffect } from 'react';
import { useParams } from 'react-router-dom';
import { InternshipService } from '../../services';
import StudentNavigation from '../student/StudentNavigation';

const InternshipDetails = () => {
  const [ internship, setInternship ] = useState({});
  const { internshipId, studentId } = useParams("");
  const [ isRegisteredToInternship, setIsRegisteredToInternship] = useState(false);

  const internshipService = new InternshipService();

  const handleRegisterInternship = () => {
    alert('You have successfully registered for the internship!');
  };
  const handleLogOutInternship = () => {
    alert('You have successfully registered for the internship!');
  };


  const fetchInternship = async () =>{
    setInternship(await internshipService.getByIdAsync(internshipId));
  } 
  const fetchIsStudentregistered = async () =>{
    setIsRegisteredToInternship(await internshipService.getIsStudentRegisteredToInternshipAsync(studentId, internshipId));
  }
  
  useEffect(() => {
    fetchInternship();
    fetchIsStudentregistered();
  }, []);

  return (
    <>
      <StudentNavigation />
      <div className="container mt-5">
        <div className="row h-100 align-items-center justify-content-center">
          <div className="col-md-10">
            <div className="card">
              <div className="card-body">
                <h2 className="card-title text-center m-2">{internship.name}</h2>
                <p className="card-text text-center mt-4"><strong>Duration: </strong> {internshipService.convertToShorterDate(internship.startDate)} - {internshipService.convertToShorterDate(internship.endDate)}</p>
                <p className="card-text text-center">{internship.description}</p>
                <p className="text-center">We are looking for students that are in {internship.studyArea ? internship.studyArea.name : ''} study area.</p>
                <div className="row">
                  <div className="col-md-12">
                    <p className="text-center mt-5"><strong>Company:</strong> {internship.company ? internship.company.name : ''}</p>
                    <p className="text-center"> {internship.company ? internship.company.address : ''}</p>
                    <p className="text-center">Check our <a href={internship.company ? internship.company.website : ''}  target="_blank" rel="noopener noreferrer">
                      website
                    </a> for more details.</p>
                  </div>
                </div>
                { isRegisteredToInternship &&
                  <div className='text-center mt-5'>
                  <button className="mx-auto" onClick={handleLogOutInternship}>
                    Odjavi ovu praksu
                  </button>
                </div>
                }
                { !isRegisteredToInternship &&
                  <div className='text-center mt-5'>
                  <button className="mx-auto" onClick={handleRegisterInternship}>
                    Prijavi se na praksu
                  </button>
                </div>
                }
              </div>
            </div>
          </div>
        </div>
      </div>
    </>
  );
};

export default InternshipDetails;

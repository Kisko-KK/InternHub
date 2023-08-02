import React, { useState, useEffect } from 'react';
import { useParams } from 'react-router-dom';
import { InternshipApplicationService, InternshipService } from '../../services';
import StudentNavigation from '../student/StudentNavigation';
import { RegisterPopupInternship } from '../../components/index'
import "../../styles/student.css"

const InternshipDetails = () => {
  const [ internship, setInternship ] = useState({});
  const { internshipId, studentId } = useParams("");
  const [ isRegisteredToInternship, setIsRegisteredToInternship] = useState(false);
  const [showPopup, setShowPopup] = useState(false);
  const [internshipApplicationId, setInternshipApplicationId] = useState('');

  const internshipService = new InternshipService();
  const internshipApplicationService = new InternshipApplicationService();

  const handleLogOutInternshipAsync = async () => {
    const withdraw = window.confirm("Are you sure you want to withdraw your application?");
    if (withdraw){
      if(await internshipApplicationService.deleteAsync(internshipApplicationId)){
        alert("Application withdrawn successfully!")
        fetchIsStudentregisteredAsync();
      }
      else{
        alert("Something has gone wrong, please try again!")
      }
    }
  };

  const fetchInternshipApplicationId = async () => {
    setInternshipApplicationId(await internshipApplicationService.getIdAsync(studentId, internshipId))
  }

  const fetchInternshipAsync = async () =>{
    setInternship(await internshipService.getByIdAsync(internshipId));
  } 
  const fetchIsStudentregisteredAsync = async () =>{
    setIsRegisteredToInternship(await internshipService.getIsStudentRegisteredToInternshipAsync(studentId, internshipId));
  }
  const handleApplySuccessAsync = async () =>{
    alert("You applied successfully!");
    await fetchInternshipApplicationId();
    await fetchIsStudentregisteredAsync();
  }
  
  useEffect(() => {
    fetchInternshipAsync();
    fetchIsStudentregisteredAsync();
    fetchInternshipApplicationId();
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
                <div className="row w-100">
                  <div className="col-md-12">
                    <p className="text-center mt-5"><strong>Company:</strong> {internship.company ? internship.company.name : ''}</p>
                    <p className="text-center"> {internship.company ? internship.company.address : ''}</p>
                    <p className="text-center">Check our <a href={internship.company ? internship.company.website : ''}  target="_blank" rel="noopener noreferrer">
                      website
                    </a> for more details.</p>
                  </div>
                </div>
                { isRegisteredToInternship &&
                  <div className='text-center mt-5 bg-c'>
                  <button className="mx-auto" onClick={handleLogOutInternshipAsync}>
                    Odjavi ovu praksu
                  </button>
                </div>
                }
                { !isRegisteredToInternship &&
                  <div className='text-center mt-5 bg-c'>
                  <button className="mx-auto" onClick={(()=>{setShowPopup(true)})}>
                    Prijavi se na praksu
                  </button>
                </div>
                }
                <RegisterPopupInternship showPopup={showPopup} handleApplySuccess={handleApplySuccessAsync} handleClose={() => {setShowPopup(false)}}/>
              </div>
            </div>
          </div>
        </div>
      </div>
    </>
  );
};

export default InternshipDetails;

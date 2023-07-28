import React from "react";
import "../styles/student.css"

export default function Internship({buttonText}) {
    return (
        <div id = "outer-container">
            <div id = "internship-container">
            <h3>Mono softwer praksa</h3>

            <div className="p-button-flex">
                <p id="description">Ovo je dobra prskj fhkjskjd fd fdf d ggd ggd g dg gddg d dggd  dg g dgd gd</p>
                <button>{buttonText}</button>
            </div>
            
            <p className="duration">Duration: 15.2.2002. - 3.7.2001.</p>

            <div className="p-flex">
                <p>Mono d.o.o</p>
                <p>Eurodom</p>
                <p>Broj prijavljenih: 23 ili </p>
            </div>
            </div>
        </div>
        
      );
}
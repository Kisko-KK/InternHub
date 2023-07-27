import React from "react";

export default function Paging({ currentPage, lastPage, onPageChanged }) {
  return (
    <ul className="pagination text-light">
      <li
        className={`page-item ${currentPage === 1 ? "disabled" : ""}`}
        onClick={() => {
          if (currentPage > 1) onPageChanged(currentPage - 1);
        }}
      >
        <span className="page-link">Prijašnje</span>
      </li>
      {lastPage <= 5 && (
        <>
          {[...Array(lastPage)].map((x, i) => (
            <li
              className={`page-item page-link${
                currentPage === i ? " active" : ""
              }`}
              onClick={() => onPageChanged(i + 1)}
            >
              {i + 1}
            </li>
          ))}
        </>
      )}
      {lastPage > 5 && (
        <>
          {currentPage <= 2 && (
            <>
              {[...Array(3)].map((x, i) => (
                <li
                  className={`page-item page-link${
                    currentPage === 1 + i ? " active" : ""
                  }`}
                  onClick={() => onPageChanged(i + 1)}
                >
                  {i + 1}
                </li>
              ))}
              <li className="page-item page-link">...</li>
              <li
                className="page-item page-link"
                onClick={() => onPageChanged(lastPage)}
              >
                {lastPage}
              </li>
            </>
          )}
          {currentPage > 2 && lastPage - 2 >= currentPage && (
            <>
              <li
                className="page-item page-link"
                onClick={() => onPageChanged(1)}
              >
                1
              </li>
              {currentPage - 2 > 1 && (
                <li className="page-item page-link">...</li>
              )}
              {[...Array(3)].map((x, i) => (
                <li
                  className={`page-item page-link${
                    currentPage === currentPage + i ? " active" : ""
                  }`}
                  onClick={() => onPageChanged(currentPage - 1 + i)}
                >
                  {currentPage - 1 + i}
                </li>
              ))}
              {currentPage + 2 < lastPage && (
                <li className="page-item page-link">...</li>
              )}
              <li
                className="page-item page-link"
                onClick={() => onPageChanged(lastPage)}
              >
                {lastPage}
              </li>
            </>
          )}
          {currentPage >= lastPage - 1 && (
            <>
              <li
                className="page-item page-link"
                onClick={() => onPageChanged(1)}
              >
                1
              </li>
              {currentPage - 2 > 1 && (
                <li className="page-item page-link">...</li>
              )}
              {[...Array(3)].map((x, i) => (
                <li
                  className={`page-item page-link${
                    lastPage === currentPage - 2 + i ? " active" : ""
                  }`}
                  onClick={() => onPageChanged(lastPage - 2 + i)}
                >
                  {lastPage - 2 + i}
                </li>
              ))}
            </>
          )}
        </>
      )}
      <li
        className={`page-item ${currentPage === lastPage ? "disabled" : ""}`}
        onClick={() => {
          if (currentPage < lastPage) onPageChanged(currentPage + 1);
        }}
      >
        <span className="page-link">Sljedeće</span>
      </li>
    </ul>
  );
}

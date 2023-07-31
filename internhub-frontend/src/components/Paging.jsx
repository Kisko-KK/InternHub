import React from "react";

export default function Paging({ currentPage, lastPage, onPageChanged }) {
  function handlePageChange(pageNumber) {
    if (
      pageNumber !== currentPage &&
      pageNumber >= 1 &&
      pageNumber <= lastPage
    ) {
      onPageChanged(pageNumber);
    }
  }

  return (
    <div className="d-flex justify-content-center align-items-center text-center my-5">
      <div style={{ height: 30 }}></div>
      <ul className="pagination text-light">
        <li
          className={`page-item ${currentPage === 1 ? "disabled" : ""}`}
          onClick={() => {
            handlePageChange(currentPage - 1);
          }}
        >
          <span className="page-link">Previous</span>
        </li>
        {lastPage <= 5 && (
          <>
            {[...Array(lastPage)].map((x, i) => (
              <li
                key={crypto.randomUUID()}
                className={`page-item page-link${
                  currentPage === i + 1 ? " active" : ""
                }`}
                onClick={() => handlePageChange(i + 1)}
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
                    key={crypto.randomUUID()}
                    className={`page-item page-link${
                      currentPage === i + 1 ? " active" : ""
                    }`}
                    onClick={() => handlePageChange(i + 1)}
                  >
                    {i + 1}
                  </li>
                ))}
                <li className="page-item page-link">...</li>
                <li
                  className={
                    "page-item page-link" +
                    (currentPage === lastPage ? " active" : "")
                  }
                  onClick={() => handlePageChange(lastPage)}
                >
                  {lastPage}
                </li>
              </>
            )}
            {currentPage > 2 && lastPage - 2 >= currentPage && (
              <>
                <li
                  className={
                    "page-item page-link" + (currentPage === 1 ? " active" : "")
                  }
                  onClick={() => handlePageChange(1)}
                >
                  1
                </li>
                {currentPage - 2 > 1 && (
                  <li className="page-item page-link">...</li>
                )}
                {[...Array(3)].map((x, i) => (
                  <li
                    key={crypto.randomUUID()}
                    className={`page-item page-link${
                      currentPage === currentPage - 1 + i ? " active" : ""
                    }`}
                    onClick={() => handlePageChange(currentPage - 1 + i)}
                  >
                    {currentPage - 1 + i}
                  </li>
                ))}
                {currentPage + 2 < lastPage && (
                  <li className="page-item page-link">...</li>
                )}
                <li
                  className={
                    "page-item page-link" +
                    (currentPage === lastPage ? " active" : "")
                  }
                  onClick={() => handlePageChange(lastPage)}
                >
                  {lastPage}
                </li>
              </>
            )}
            {currentPage >= lastPage - 1 && (
              <>
                <li
                  className={
                    "page-item page-link" + (currentPage === 1 ? " active" : "")
                  }
                  onClick={() => handlePageChange(1)}
                >
                  1
                </li>
                {currentPage - 2 > 1 && (
                  <li className="page-item page-link">...</li>
                )}
                {[...Array(3)].map((x, i) => (
                  <li
                    key={crypto.randomUUID()}
                    className={`page-item page-link${
                      currentPage === lastPage - 2 + i ? " active" : ""
                    }`}
                    onClick={() => handlePageChange(lastPage - 2 + i)}
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
            if (currentPage < lastPage) handlePageChange(currentPage + 1);
          }}
        >
          <span className="page-link">Next</span>
        </li>
      </ul>
    </div>
  );
}

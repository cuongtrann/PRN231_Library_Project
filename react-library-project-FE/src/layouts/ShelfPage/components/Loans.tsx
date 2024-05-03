import { useOktaAuth } from "@okta/okta-react";
import { useEffect, useState } from "react";
import { ShelfCurrentLoans } from "../../../models/ShelfCurrentLoans";
import { error } from "console";
import { SpinnerLoading } from "../../Utils/SpinnerLoading";
import { Link } from "react-router-dom";
import { LoansModal } from "../LoansModal";

export const Loans = () => {
  const { authState } = useOktaAuth();
  const [httpError, setHttpError] = useState(null);

  //Current Loans
  const [shelfCurrentLoans, setShelfCurrentLoans] = useState<
    ShelfCurrentLoans[]
  >([]);
  const [isLoadingUserLoans, setIsLoadingUserLoans] = useState(true);
  const [checkout, setCheckout] = useState(false);

  useEffect(() => {
    const fetchUserCurrentLoans = async () => {
      if (authState && authState.isAuthenticated) {
        const url = "http://localhost:5000/api/books/secure/currentloans";
        const requestOption = {
          method: "GET",
          headers: {
            Authorization: `Bearer ${authState.accessToken?.accessToken}`,
            "Content-Type": "application/json",
          },
        };
        const shelfCurrentLoansResponse = await fetch(url, requestOption);
        if (!shelfCurrentLoansResponse.ok) {
          throw new Error("Some thing went wrong");
        }
        const shelfCurrentLoansResponseJson =
          await shelfCurrentLoansResponse.json();
        setShelfCurrentLoans(shelfCurrentLoansResponseJson);
      }
      setIsLoadingUserLoans(false);
    };
    fetchUserCurrentLoans().catch((error: any) => {
      setIsLoadingUserLoans(false);
      setHttpError(error.message);
    });
    window.scrollTo(0, 0);
  }, [authState, checkout]);

  if (isLoadingUserLoans) {
    return <SpinnerLoading />;
  }

  if (httpError) {
    return (
      <div className="container m-5">
        <p>{httpError}</p>
      </div>
    );
  }

  async function returnBook(bookId: number) {
    const url = `http://localhost:5000/api/books/secure/return/?bookId=${bookId}`;
    const requestOption = {
      method: "PUT",
      headers: {
        Authorization: `Bearer ${authState?.accessToken?.accessToken}`,
        "Content-Type": "application/json",
      },
    };
    const returnResponse = await fetch(url, requestOption);
    if (!returnResponse.ok) {
      throw new Error("Some thing went wrong");
    }
    setCheckout(!checkout);
  }

  async function renewLoan(bookId: number) {
    const url = `http://localhost:5000/api/books/secure/renew/loan/?bookId=${bookId}`;
    const requestOption = {
      method: "PUT",
      headers: {
        Authorization: `Bearer ${authState?.accessToken?.accessToken}`,
        "Content-Type": "application/json",
      },
    };
    const renewResponse = await fetch(url, requestOption);
    if (!renewResponse.ok) {
      throw new Error("Some thing went wrong");
    }
    setCheckout(!checkout);
  }

  return (
    <div>
      {/*Desktop*/}
      <div className="d-none d-lg-block mt-2">
        {shelfCurrentLoans.length > 0 ? (
          <>
            <h5>Current Loans: </h5>
            {shelfCurrentLoans.map((shelfCurrentLoans) => (
              <div key={shelfCurrentLoans.book.id}>
                <div className="row mt-3 mb-3">
                  <div className="col-4 col-md-4 container">
                    {shelfCurrentLoans.book.img ? (
                      <img
                        src={shelfCurrentLoans.book.img}
                        width="226"
                        height="349"
                        alt="Book"
                      />
                    ) : (
                      <img
                        src="./../../../Images/BooksImages/book-luv2code-1000.png"
                        width="226"
                        height="349"
                        alt="Book"
                      />
                    )}
                  </div>
                  <div className="card col-3 col-md-3 container d-flex">
                    <div className="card-body">
                      <div className="mt-3">
                        <h4>Loan Options</h4>
                        {shelfCurrentLoans.daysLeft > 0 && (
                          <p className="text-secondary">
                            Due in {shelfCurrentLoans.daysLeft} days.
                          </p>
                        )}

                        {shelfCurrentLoans.daysLeft === 0 && (
                          <p className="text-success">Due today.</p>
                        )}

                        {shelfCurrentLoans.daysLeft < 0 && (
                          <p className="text-danger">
                            Past due by {shelfCurrentLoans.daysLeft} days.
                          </p>
                        )}
                        <div className="list-group mt-3">
                          <button
                            className="list-group-item list-group-item-action"
                            aria-current="true"
                            data-bs-toggle="modal"
                            data-bs-target={`#modal${shelfCurrentLoans.book.id}`}
                          >
                            Manage Loan
                          </button>
                          <Link
                            to={`read/${shelfCurrentLoans.book.id}`}
                            className="list-group-item list-group-item-action"
                          >
                            Read book
                          </Link>

                          <Link
                            to={"search"}
                            className="list-group-item list-group-item-action"
                          >
                            Search more books?
                          </Link>
                        </div>
                      </div>
                      <hr />
                      <p className="mt-3">
                        Help other find their adventure by reviewing your loan.
                      </p>
                      <Link
                        className="btn btn-primary"
                        to={`/checkout/${shelfCurrentLoans.book.id}`}
                      >
                        Leave a review
                      </Link>
                    </div>
                  </div>
                </div>
                <hr />
                <LoansModal
                  shelfCurrentLoan={shelfCurrentLoans}
                  mobile={false}
                  returnBook={returnBook}
                  renewLoan={renewLoan}
                />
              </div>
            ))}
          </>
        ) : (
          <>
            <h3 className="mt-3">Currently no loans</h3>
            <Link className="btn btn-primary" to={"search"}>
              Search for a new book
            </Link>
          </>
        )}
      </div>
      {/*Mobile */}

      <div className="container d-lg-none mt-2">
        {shelfCurrentLoans.length > 0 ? (
          <>
            <h5 className="mb-3">Current Loans: </h5>
            {shelfCurrentLoans.map((shelfCurrentLoans) => (
              <div key={shelfCurrentLoans.book.id}>
                <div className="d-flex justify-content-center align-items-center">
                  {shelfCurrentLoans.book.img ? (
                    <img
                      src={shelfCurrentLoans.book.img}
                      width="226"
                      height="349"
                      alt="Book"
                    />
                  ) : (
                    <img
                      src="./../../../Images/BooksImages/book-luv2code-1000.png"
                      width="226"
                      height="349"
                      alt="Book"
                    />
                  )}
                </div>
                <div className="card d-flex mt-5 mb-3">
                  <div className="card-body container">
                    <div className="mt-3">
                      <h4>Loan Options</h4>
                      {shelfCurrentLoans.daysLeft > 0 && (
                        <p className="text-secondary">
                          Due in {shelfCurrentLoans.daysLeft} days.
                        </p>
                      )}

                      {shelfCurrentLoans.daysLeft === 0 && (
                        <p className="text-success">Due today.</p>
                      )}

                      {shelfCurrentLoans.daysLeft < 0 && (
                        <p className="text-danger">
                          Past due by {shelfCurrentLoans.daysLeft} days.
                        </p>
                      )}
                      <div className="list-group mt-3">
                        <button
                          className="list-group-item list-group-item-action"
                          aria-current="true"
                          data-bs-toggle="modal"
                          data-bs-target={`#mobilemodal${shelfCurrentLoans.book.id}`}
                        >
                          Manage Loan
                        </button>
                        <Link
                          to={`read/${shelfCurrentLoans.book.id}`}
                          className="list-group-item list-group-item-action"
                        >
                          Read book
                        </Link>
                        <Link
                          to={"search"}
                          className="list-group-item list-group-item-action"
                        >
                          Search more books?
                        </Link>
                      </div>
                    </div>
                    <hr />
                    <p className="mt-3">
                      Help other find their adventure by reviewing your loan.
                    </p>
                    <Link
                      className="btn btn-primary"
                      to={`/checkout/${shelfCurrentLoans.book.id}`}
                    >
                      Leave a review
                    </Link>
                  </div>
                </div>

                <hr />
                <LoansModal
                  shelfCurrentLoan={shelfCurrentLoans}
                  mobile={true}
                  returnBook={returnBook}
                  renewLoan={renewLoan}
                />
              </div>
            ))}
          </>
        ) : (
          <>
            <h3 className="mt-3">Currently no loans</h3>
            <Link className="btn btn-primary" to={"search"}>
              Search for a new book
            </Link>
          </>
        )}
      </div>
    </div>
  );
};

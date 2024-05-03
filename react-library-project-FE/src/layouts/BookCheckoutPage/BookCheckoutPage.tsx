import { useEffect, useState } from "react";
import { BookModel } from "../../models/BookModel";
import { SpinnerLoading } from "../Utils/SpinnerLoading";
import { StarsReview } from "../Utils/StarsReview";
import { CheckoutAndReviewBox } from "./CheckoutAndReviewBox";
import { ReviewModel } from "../../models/ReviewModel";
import { error } from "console";
import { LatestReviews } from "./LatestReviews";
import { useOktaAuth } from "@okta/okta-react";
import ReviewRequestModel from "../../models/ReviewRequestModel";

export const BookCheckoutPage = () => {
  const [book, setBook] = useState<BookModel>();
  const [isLoading, setIsLoading] = useState(true);
  const [httpError, setHttpError] = useState(null);

  // Review State
  const [reviews, setReviews] = useState<ReviewModel[]>([]);
  const [totalStars, setTotalStars] = useState(0);
  const [isLoadingReivew, setIsLoadingReview] = useState(true);

  // Current Loan
  const { authState } = useOktaAuth();
  const [currentLoanCount, setCurrentLoanCount] = useState(0);
  const [isLoadingLoanCount, setIsLoadingLoanCount] = useState(true);

  // is check out
  const [isCheckout, setIsCheckout] = useState(false);
  const [isLoadingIsCheckout, setIsLoadingIsCheckout] = useState(true);
  const bookId = window.location.pathname.split("/")[2];

  // review
  const [isReviewLeft, setIsReviewLeft] = useState(false);
  const [isLoadingUserReview, setIsLoadingUserReview] = useState(true);

  // Payment
  const [displayError, setDisplayError] = useState(false);

  useEffect(() => {
    const fetchBook = async () => {
      const baseUrl: string = `http://localhost:5000/api/books/${bookId}`;

      const response = await fetch(baseUrl);

      if (!response.ok) {
        throw new Error("Some thing went wrong!");
      }

      const responseJson = await response.json();

      const loadedBook: BookModel = {
        id: responseJson.id,
        title: responseJson.title,
        author: responseJson.author,
        description: responseJson.description,
        copies: responseJson.copies,
        copiesAvailable: responseJson.copiesAvailable,
        category: responseJson.category,
        img: responseJson.img,
      };

      setBook(loadedBook);
      setIsLoading(false);
    };
    fetchBook().catch((error: any) => {
      setIsLoading(false);
      setHttpError(error.message);
    });
  }, [isCheckout]);

  // useEffect review
  useEffect(() => {
    const fetchBookReview = async () => {
      const reviewUrl: string = `http://localhost:5000/api/reviews/search/findByBookId?bookId=${bookId}`;
      const responseReview = await fetch(reviewUrl);
      if (!responseReview.ok) {
        throw new Error("Some thing went wrong");
      }
      const responseReviewJson = await responseReview.json();
      const responseData = responseReviewJson.reviews;
      const loadedReviews: ReviewModel[] = [];
      let weightStarReviews: number = 0;
      for (const key in responseData) {
        loadedReviews.push({
          id: responseData[key].id,
          userEmail: responseData[key].userEmail,
          date: responseData[key].date,
          rating: responseData[key].rating,
          book_id: responseData[key].bookId,
          reviewDescription: responseData[key].reviewDescription,
        });
        weightStarReviews += responseData[key].rating;
      }
      if (loadedReviews) {
        const round = (
          Math.round((weightStarReviews / loadedReviews.length) * 2) / 2
        ).toFixed(1);
        setTotalStars(Number(round));
      }
      setReviews(loadedReviews);
      setIsLoadingReview(false);
    };

    fetchBookReview().catch((error: any) => {
      setIsLoadingReview(false);
      setHttpError(error.message);
    });
  }, [isReviewLeft]);

  // useEffect loanCount
  useEffect(() => {
    const fetchLoanCount = async () => {
      if (authState && authState.isAuthenticated) {
        const loanCountUrl: string =
          "http://localhost:5000/api/books/secure/currentloans/count";
        const requestOption = {
          method: "GET",
          headers: {
            Authorization: `Bearer ${authState?.accessToken?.accessToken}`,
            "Content-Type": "application/json",
          },
        };
        const currentLoanResponse = await fetch(loanCountUrl, requestOption);
        if (!currentLoanResponse.ok) {
          throw new Error("Some thing went wrong");
        }
        const currentLoanJson = await currentLoanResponse.json();
        setCurrentLoanCount(Number(currentLoanJson));
      }
      setIsLoadingLoanCount(false);
    };
    fetchLoanCount().catch((error: any) => {
      setIsLoadingLoanCount(false);
      setHttpError(error.message);
    });
  }, [authState, isCheckout]);

  // useEffect isCheckout
  useEffect(() => {
    const fetchIsCheckout = async () => {
      if (authState && authState.isAuthenticated) {
        const IsCheckoutUrl = `http://localhost:5000/api/books/secure/ischeckedout/byuser?bookId=${bookId}`;
        const requestOption = {
          method: "GET",
          headers: {
            Authorization: `Bearer ${authState?.accessToken?.accessToken}`,
            "Content-Type": "application/json",
          },
        };
        const isCheckoutResponse = await fetch(IsCheckoutUrl, requestOption);
        if (!isCheckoutResponse.ok) {
          throw new Error("Some thing went wrong");
        }
        const isCheckoutResponseJson = await isCheckoutResponse.json();
        setIsCheckout(isCheckoutResponseJson);
      }
      setIsLoadingIsCheckout(false);
    };

    fetchIsCheckout().catch((error: any) => {
      setIsLoadingIsCheckout(false);
      setHttpError(error.message);
    });
  }, [authState]);

  // useEffect checkReviewByUser
  useEffect(() => {
    const fetchUserReview = async () => {
      if (authState && authState.isAuthenticated) {
        const checkUserReviewUrl = `http://localhost:5000/api/reviews/secure/user/book?bookId=${bookId}`;
        const requestOption = {
          method: "GET",
          headers: {
            Authorization: `Bearer ${authState?.accessToken?.accessToken}`,
            "Content-Type": "Application/json",
          },
        };
        const response = await fetch(checkUserReviewUrl, requestOption);
        if (!response.ok) {
          throw new Error("Some thing went wrong");
        }
        const responseJson = await response.json();
        setIsReviewLeft(responseJson);
      }
      setIsLoadingUserReview(false);
    };
    fetchUserReview().catch((error: any) => {
      setIsLoadingUserReview(false);
      setHttpError(error.message);
    });
  }, [authState]);
  if (
    isLoading ||
    isLoadingReivew ||
    isLoadingLoanCount ||
    isLoadingIsCheckout ||
    isLoadingUserReview
  ) {
    return <SpinnerLoading />;
  }

  if (httpError) {
    return (
      <div className="container m-5">
        <p>{httpError}</p>
      </div>
    );
  }

  async function checkoutBook() {
    try {
      const url = `http://localhost:5000/api/books/secure/checkout/?bookId=${book?.id}`;
      const requestOption = {
        method: "PUT",
        headers: {
          Authorization: `Bearer ${authState?.accessToken?.accessToken}`,
          "Content-Type": "application/json",
        },
      };
      const checkoutResponse = await fetch(url, requestOption);
      if (!checkoutResponse.ok) {
        setDisplayError(true);
        throw new Error("Some thing went wrong");
      }
      setDisplayError(false);
      setIsCheckout(true);
    } catch {
      setDisplayError(true);
    }
  }

  async function submitReview(starInput: number, reviewDescription: string) {
    let bookId: number = 0;
    if (book?.id) {
      bookId = book.id;
    }

    const reviewRequestModel = new ReviewRequestModel(
      starInput,
      bookId,
      reviewDescription
    );

    const url = `http://localhost:5000/api/reviews/secure`;
    const requestOption = {
      method: "POST",
      headers: {
        Authorization: `Bearer ${authState?.accessToken?.accessToken}`,
        "Content-Type": "application/json",
      },
      body: JSON.stringify(reviewRequestModel),
    };
    const returnResponse = await fetch(url, requestOption);
    if (!returnResponse.ok) {
      throw new Error("Some thing went wrong!");
    }
    setIsReviewLeft(true);
  }

  return (
    <div>
      <div className="container d-none d-lg-block">
        {displayError && (
          <div className="alert alert-danger mt-3" role="alert">
            Please pay outstanding fees and/or return late book(s).
          </div>
        )}
        <div className="row mt-5">
          <div className="col-sm-2 col-md-2">
            {book?.img ? (
              <img src={book?.img} width="226" height="349" alt="Book" />
            ) : (
              <img
                src={require("./../../Images/BooksImages/book-luv2code-1000.png")}
                width="226"
                height="349"
                alt="Book"
              />
            )}
          </div>
          <div className="col-4 col-md-4 container">
            <div className="ml-2">
              <h2>{book?.title}</h2>
              <h5 className="text-primary">{book?.author}</h5>
              <p className="lead">{book?.description}</p>
              <StarsReview rating={totalStars} size={32} />
            </div>
          </div>
          <CheckoutAndReviewBox
            submitReview={submitReview}
            loanCount={currentLoanCount}
            book={book}
            mobile={false}
            isCheckout={isCheckout}
            isAuthenticated={authState?.isAuthenticated}
            checkoutBook={checkoutBook}
            isReviewLeft={isReviewLeft}
          />
        </div>
        <hr />
        <LatestReviews reviews={reviews} bookId={book?.id} mobile={false} />
      </div>
      <div className="container d-lg-none mt-5">
        {displayError && (
          <div className="alert alert-danger mt-3" role="alert">
            Please pay outstanding fees and/or return late book(s).
          </div>
        )}
        <div className="d-flex justify-content-center align-items-center">
          {book?.img ? (
            <img src={book?.img} width="226" height="349" alt="Book" />
          ) : (
            <img
              src={require("./../../Images/BooksImages/book-luv2code-1000.png")}
              width="226"
              height="349"
              alt="Book"
            />
          )}
        </div>
        <div className="mt-4">
          <div className="ml-2">
            <h2>{book?.title}</h2>
            <h5 className="text-primary">{book?.author}</h5>
            <p className="lead">{book?.description}</p>
          </div>
        </div>
        <CheckoutAndReviewBox
          loanCount={currentLoanCount}
          book={book}
          mobile={true}
          isCheckout={isCheckout}
          isAuthenticated={authState?.isAuthenticated}
          checkoutBook={checkoutBook}
          isReviewLeft={isReviewLeft}
          submitReview={submitReview}
        />

        <hr />
        <LatestReviews reviews={reviews} bookId={book?.id} mobile={true} />
      </div>
    </div>
  );
};

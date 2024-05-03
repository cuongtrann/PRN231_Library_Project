import { useEffect, useState } from "react";
import { BookModel } from "../../models/BookModel";
import { useOktaAuth } from "@okta/okta-react";
import PDFViewer from "../PDFViewer";
import { SpinnerLoading } from "../Utils/SpinnerLoading";

export const ReadBookPage = () => {
  const [book, setBook] = useState<BookModel>();
  const [isLoading, setIsLoading] = useState(true);
  const [httpError, setHttpError] = useState(null);
  const [isCheckout, setIsCheckout] = useState(false);
  const [isLoadingIsCheckout, setIsLoadingIsCheckout] = useState(true);

  const { authState } = useOktaAuth();
  const bookId = window.location.pathname.split("/")[2];

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
      loadedBook.bookContent = responseJson.bookContent;

      setBook(loadedBook);
      setIsLoading(false);
    };
    fetchBook().catch((error: any) => {
      setIsLoading(false);
      setHttpError(error.message);
    });
  }, [authState]);

  if (isLoading || isLoadingIsCheckout) {
    return <SpinnerLoading />;
  }

  if (httpError) {
    return (
      <div className="container m-5">
        <p>{httpError}</p>
      </div>
    );
  }

  return (
    <div className="container mt-3">
      {isCheckout ? (
        <>
          <h2>
            {book?.title} - {book?.author}
          </h2>
          <PDFViewer urlFile={book?.bookContent} />
        </>
      ) : (
        <>
          <h2>You haven't checkout this book</h2>
        </>
      )}
    </div>
  );
};

import { useOktaAuth } from "@okta/okta-react";
import { read } from "fs";
import { ChangeEvent, useState } from "react";
import { AddBookRequest } from "../../../models/AddBookRequest";
import { SpinnerLoading } from "../../Utils/SpinnerLoading";

export const AddNewBook = () => {
  const { authState } = useOktaAuth();

  // New Book
  const [title, setTitle] = useState("");
  const [author, setAuthor] = useState("");
  const [description, setDescription] = useState("");
  const [copies, setCopies] = useState(0);
  const [category, setCategory] = useState("Category");
  const [selectedImage, setSelectedImage] = useState<any>(null);
  const [selectedFile, setSelectedFile] = useState<any>(null);
  const [submitDisabled, setSubmitDisabled] = useState(false);

  // Display
  const [displayWarning, setDisplayWarning] = useState(false);
  const [displaySuccess, setDisplaySuccess] = useState(false);

  function categoryField(value: string) {
    setCategory(value);
  }

  async function base64ConversionForImages(e: any) {
    if (e.target.files[0]) {
      getBase64(e.target.files[0]);
    }

    function getBase64(file: any) {
      let reader = new FileReader();
      reader.readAsDataURL(file);
      reader.onload = function () {
        setSelectedImage(reader.result);
        console.log(reader.result);
      };
      reader.onerror = function (error) {
        console.log("Error", error);
      };
    }
  }

  const handleFileChange = async (event: ChangeEvent<HTMLInputElement>) => {
    const file = event.target.files?.[0];
    setSelectedFile(file || null);
  };

  async function submitNewBook() {
    setSubmitDisabled(true);
    const url = `http://localhost:5000/api/admin/secure/add/book`;
    if (
      authState?.isAuthenticated &&
      title !== "" &&
      author !== "" &&
      category !== "Category" &&
      description !== "" &&
      copies > 0
    ) {
      const book: AddBookRequest = new AddBookRequest(
        title,
        author,
        description,
        copies,
        category
      );
      book.img = selectedImage;
      const formData = new FormData();
      formData.append("book", JSON.stringify(book));
      formData.append("file", selectedFile);
      console.log(formData);
      const requestOptions = {
        method: "POST",
        headers: {
          Authorization: `Bearer ${authState.accessToken?.accessToken}`,
        },
        body: formData,
      };
      const submitNewBookResponse = await fetch(url, requestOptions);
      if (!submitNewBookResponse.ok) {
        throw new Error("Some thing went wrong");
        setSubmitDisabled(false);
      }

      setTitle("");
      setAuthor("");
      setDescription("");
      setCopies(0);
      setCategory("Category");
      setSelectedImage(null);
      setSelectedFile(null);
      setDisplaySuccess(true);
      setDisplayWarning(false);
      setSubmitDisabled(false);
    } else {
      setDisplaySuccess(false);
      setDisplayWarning(true);
      setSubmitDisabled(false);
    }
  }
  return (
    <div className="container mt-5 mb-5">
      {displaySuccess && (
        <div className="alert alert-success" role="alert">
          Book added successfully
        </div>
      )}
      {displayWarning && (
        <div className="alert alert-danger" role="alert">
          All field must be filled out
        </div>
      )}
      <div className="card">
        <div className="card-header">Add a new book</div>
        <div className="card-body">
          <form method="POST">
            <div className="row">
              <div className="col-md-6 mb-3">
                <label className="form-label">Title</label>
                <input
                  type="text"
                  className="form-control"
                  name="title"
                  required
                  onChange={(e) => setTitle(e.target.value)}
                  value={title}
                />
              </div>
              <div className="col-md-3 mb-3">
                <label className="form-label">Author</label>
                <input
                  type="text"
                  className="form-control"
                  name="author"
                  required
                  onChange={(e) => setAuthor(e.target.value)}
                  value={author}
                />
              </div>
              <div className="col-md-3 mb-3">
                <label className="form-label">Category</label>
                <button
                  className="form-control btn btn-secondary dropdown-toggle"
                  type="button"
                  id="dropdownMenuButton1"
                  data-bs-toggle="dropdown"
                  aria-expanded="false"
                >
                  {category}
                </button>
                <ul
                  id="addNewBookId"
                  className="dropdown-menu"
                  aria-labelledby="dropdownMenuButton1"
                >
                  <li>
                    <a
                      onClick={() => categoryField("Self help")}
                      className="dropdown-item"
                    >
                      Self help
                    </a>
                  </li>
                  <li>
                    <a
                      onClick={() => categoryField("Minimalism")}
                      className="dropdown-item"
                    >
                      Minimalism
                    </a>
                  </li>
                  <li>
                    <a
                      onClick={() => categoryField("Productive")}
                      className="dropdown-item"
                    >
                      Productive
                    </a>
                  </li>
                  <li>
                    <a
                      onClick={() => categoryField("Science")}
                      className="dropdown-item"
                    >
                      Science
                    </a>
                  </li>
                  <li>
                    <a
                      onClick={() => categoryField("Novel")}
                      className="dropdown-item"
                    >
                      Novel
                    </a>
                  </li>
                </ul>
              </div>
            </div>
            <div className="col-md-12 mb-3">
              <label className="form-label">Description</label>
              <textarea
                className="form-control"
                id="exampleFormControlTextarea1"
                rows={3}
                onChange={(e) => setDescription(e.target.value)}
                value={description}
              ></textarea>
            </div>
            <div className="col-md-3 mb-3">
              <label className="form-label">Copies</label>
              <input
                type="number"
                className="form-control"
                name="Copies"
                required
                onChange={(e) => setCopies(Number(e.target.value))}
                value={copies}
              />
            </div>
            <div className="col-md-3 mb-3">
              <label className="form-label">Image</label>
              <input
                type="file"
                onChange={(e) => base64ConversionForImages(e)}
              />
            </div>

            <div className="col-md-3 mb-3">
              <label className="form-label">eBookFile</label>
              <input type="file" onChange={handleFileChange} />
            </div>

            <div>
              <button
                onClick={submitNewBook}
                type="button"
                className="btn btn-primary mt-3"
                disabled={submitDisabled}
              >
                Add Book
              </button>
            </div>
          </form>
          {submitDisabled && <SpinnerLoading />}
        </div>
      </div>
    </div>
  );
};

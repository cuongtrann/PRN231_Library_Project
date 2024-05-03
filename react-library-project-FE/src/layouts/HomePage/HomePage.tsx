import { Carousel } from "./components/Carousel";
import { ExploreTopBooks } from "./components/ExploreTopBook";
import { Heros } from "./components/Heros";
import { LibraryServie } from "./components/LibraryServices";

export const HomePage = () => {
  return (
    <>
      <ExploreTopBooks />
      <Carousel />
      <Heros />
      <LibraryServie />
    </>
  );
};

import { useAppContext } from "./Context/AppContext";
import AppSidePanel from "./Components/AppSidePanel";
import LoadingPanel from "./Panel/LoadingPanel";

function App() {
  const { isLoading, activePanel } = useAppContext();

  if (isLoading) {
    return <LoadingPanel />;
  }

  return (
    <div className="fixed inset-0  flex">
      <AppSidePanel />
      {activePanel}
    </div>
  );
}

export default App;

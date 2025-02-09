import "../gradient.shadow.css";

export default function GradientShadowBox({ children }) {
  return (
    <div className="box" style={{ color: "red" }}>
      {children}
    </div>
  );
}

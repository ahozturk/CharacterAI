/** @type {import('tailwindcss').Config} */
module.exports = {
  content: ["./Views/**/*.{html,js,cshtml,razor}"],
  theme: {
    extend: {},
  },
  plugins: [
    require("daisyui"),
  ],
  daisyui: {
    themes: ["light"],
  },
}


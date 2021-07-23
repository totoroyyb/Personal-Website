const colors = require('tailwindcss/colors')

module.exports = {
    mode: 'jit',
    purge: [
        './wwwroot/index.html',
        './**/*.razor',
    ],
    darkMode: 'class',
    theme: {
        extend: {
            fontSize: {
                'ms': '.82rem'
            },
            colors: {
                cyan: colors.cyan
            },
            boxShadow: {
                't-sm': '0 -1px 2px 0 rgba(0, 0, 0, 0.05)',
                't-md': '0 -4px 6px -1px rgba(0, 0, 0, 0.1), 0 2px 4px -1px rgba(0, 0, 0, 0.06)',
                't-lg': '0 -10px 15px -3px rgba(0, 0, 0, 0.1), 0 4px 6px -2px rgba(0, 0, 0, 0.05)',
                't-xl': '0 -20px 25px -5px rgba(0, 0, 0, 0.1), 0 10px 10px -5px rgba(0, 0, 0, 0.04)',
                't-2xl': '0 -25px 50px -12px rgba(0, 0, 0, 0.25)',
                't-3xl': '0 -35px 60px -15px rgba(0, 0, 0, 0.3)',
            },
            backgroundImage: theme => ({
                'contact-bg': "url('../images/coding.png')",
                'test-bg': "url('../images/coding.png')",
            }),
            backgroundSize: {
                'auto': 'auto',
                'cover': 'cover',
                'contain': 'contain',
                '50%': '50%',
                '16': '4rem',
            }
        },
    },
    variants: {
        extend: {},
    },
    plugins: [
        require('tailwindcss-textshadow')
    ]
}